import { render } from '@testing-library/react';
import Employees from './Employees';
import { vi } from 'vitest';

test('Displays correct employee data', () => {
    // Mock setEmployees function
    const setEmployeesMock = vi.fn();

    // Sample employee data
    const employees = [
        { id: 1, name: 'John Doe', email: 'john@example.com', dateOfBirth: '2020-01-01', department: 'IT' },
        { id: 2, name: 'Jane Doe', email: 'jane@example.com', dateOfBirth: '2021-02-02', department: 'HR' }
    ];

    // Render the component
    const { getByText } = render(<Employees employees={employees} setEmployees={setEmployeesMock} />);

    // Assert that the edit modal contains the correct employee data
    expect(getByText('John Doe')).toBeInTheDocument();
    expect(getByText('john@example.com')).toBeInTheDocument();
    expect(getByText('2020-01-01')).toBeInTheDocument();
    expect(getByText('IT')).toBeInTheDocument();
});
