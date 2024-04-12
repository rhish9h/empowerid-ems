import { render, fireEvent, waitFor } from '@testing-library/react';
import EditEmployee from './EditEmployee';
import { BACKEND_URL } from '../../constants';
import { vi, expect } from 'vitest';

test('Submitting form calls fetch to update employee', async () => {
    // Mock the fetch function
    globalThis.fetch = vi.fn().mockResolvedValueOnce({ ok: true });

    // Mock the setEmployees function
    const setEmployeesMock = vi.fn();

    // Render the component
    const { getByLabelText, getByText } = render(
        <EditEmployee
            openModal={true}
            setOpenModal={vi.fn()}
            employee={{ id: 1, name: 'John Doe', email: 'john@example.com', dateOfBirth: '2020-03-06', department: 'IT' }}
            setEmployees={setEmployeesMock}
        />
    );

    // Fill out form fields
    fireEvent.change(getByLabelText('Name'), { target: { value: 'Jane Doe' } });
    fireEvent.change(getByLabelText('Email'), { target: { value: 'jane@example.com' } });
    fireEvent.change(getByLabelText('Date of birth'), { target: { value: '2022-04-10' } });
    fireEvent.change(getByLabelText('Department'), { target: { value: 'HR' } });

    // Submit form
    fireEvent.click(getByText('Submit'));

    // Wait for the form submission to complete
    await waitFor(() => {
        // Check if fetch is called with the updated employee data
        expect(fetch).toHaveBeenCalledWith(`${BACKEND_URL}/api/employees/1`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                name: 'Jane Doe',
                email: 'jane@example.com',
                dateOfBirth: '2022-04-10',
                department: 'HR'
            })
        });
    });
});
