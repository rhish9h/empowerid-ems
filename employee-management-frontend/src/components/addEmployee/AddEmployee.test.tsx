import { render, fireEvent } from '@testing-library/react';
import AddEmployee from './AddEmployee';
import { expect, vi } from 'vitest'
import { BACKEND_URL } from '../../constants';

describe('AddEmployee Component', () => {
    test('Form submission adds a new employee', async () => {
        // Mock the fetch function
        globalThis.fetch = vi.fn();

        // Mock the setEmployees function
        const setEmployeesMock = vi.fn();

        // Render the component
        const { getByLabelText, getByText } = render(<AddEmployee setEmployees={setEmployeesMock} />);

        // Fill out form fields
        fireEvent.change(getByLabelText('Name'), { target: { value: 'John Doe' } });
        fireEvent.change(getByLabelText('Email'), { target: { value: 'john@example.com' } });
        fireEvent.change(getByLabelText('Date of birth'), { target: { value: '2020-03-06' } });
        fireEvent.change(getByLabelText('Department'), { target: { value: 'IT' } });

        // Submit form
        fireEvent.click(getByText('Submit'));

        // Check if fetch is called once
        expect(fetch).toHaveBeenCalledTimes(1);
        expect(fetch).toHaveBeenCalledWith(BACKEND_URL + "/api/employees", {
            method: 'POST',
            "body": "{\"name\":\"John Doe\",\"email\":\"john@example.com\",\"dateOfBirth\":\"2020-03-06T00:00:00.000Z\",\"department\":\"IT\"}",
            "headers": {
                "Content-Type": "application/json",
            },
        });
    });
});
