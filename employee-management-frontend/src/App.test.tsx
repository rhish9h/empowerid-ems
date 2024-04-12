import { render, waitFor } from '@testing-library/react';
import App from './App';
import { vi } from 'vitest';

test('App fetches and displays employees correctly', async () => {
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

  // Render the component
  const { getByText } = render(<App />);

  // Wait for the employees to be fetched and displayed
  await waitFor(() => {
    // Check if the employee information is displayed correctly
    expect(getByText('John Doe')).toBeInTheDocument();
    expect(getByText('john@example.com')).toBeInTheDocument();
    expect(getByText('2000-01-01')).toBeInTheDocument();
    expect(getByText('IT')).toBeInTheDocument();
  });
});
