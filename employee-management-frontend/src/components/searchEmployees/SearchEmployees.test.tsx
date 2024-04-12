import { render, fireEvent, waitFor } from '@testing-library/react';
import SearchEmployees from './SearchEmployees';
import { BACKEND_URL } from '../../constants';

test('Submitting search form calls fetch to fetch employees', async () => {
    // Mock the fetch function
    globalThis.fetch = vi.fn().mockResolvedValueOnce({
        ok: true,
        json: async () => ([{
            id: 1,
            name: 'John Doe',
            email: 'john@example.com',
            dateOfBirth: '2000-01-01',
            department: 'IT'
        }])
    });

    // Mock the setEmployees function
    const setEmployeesMock = vi.fn();

    // Render the component
    const { getByLabelText, getByText } = render(
        <SearchEmployees setEmployees={setEmployeesMock} />
    );

    // Fill out form fields
    fireEvent.change(getByLabelText('Name'), { target: { value: 'Jane Doe' } });
    fireEvent.change(getByLabelText('Email'), { target: { value: 'jane@example.com' } });
    fireEvent.change(getByLabelText('Department'), { target: { value: 'HR' } });

    // Submit form
    fireEvent.click(getByText('Search'));

    // Wait for the form submission to complete
    await waitFor(() => {
        // Check if fetch is called with the appropriate URL and method
        expect(fetch).toHaveBeenCalledWith(`${BACKEND_URL}/api/employees/search?&name=Jane%20Doe&email=jane%40example.com&department=HR`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        // Check if setEmployees is called with the fetched employees
        expect(setEmployeesMock).toHaveBeenCalledWith([{
            id: 1,
            name: 'John Doe',
            email: 'john@example.com',
            dateOfBirth: '2000-01-01',
            department: 'IT'
        }]);
    });
});
