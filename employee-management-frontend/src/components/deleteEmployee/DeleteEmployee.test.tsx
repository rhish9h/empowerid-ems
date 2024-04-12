import { render, fireEvent, waitFor } from '@testing-library/react';
import { expect, vi } from 'vitest'
import DeleteEmployee from './DeleteEmployee';
import { BACKEND_URL } from '../../constants';

test('Clicking "Yes, I\'m sure" button triggers delete operation', async () => {
    // Mock the fetch function
    globalThis.fetch = vi.fn();

    // Mock the setEmployees function
    const setEmployeesMock = vi.fn();

    // Render the component
    const { getByText } = render(
        <DeleteEmployee
            openModal={true}
            setOpenModal={vi.fn()}
            employee={{ id: 1, name: 'John Doe', email: 'john@example.com', dateOfBirth: '2020-03-06', department: 'IT' }}
            setEmployees={setEmployeesMock}
        />
    );

    // Click the "Yes, I'm sure" button
    fireEvent.click(getByText("Yes, I'm sure"));
    expect(fetch).toHaveBeenCalledWith(BACKEND_URL + "/api/employees/1", {
        method: 'DELETE',
        "headers": {
            "Content-Type": "application/json",
        },
    });
});
