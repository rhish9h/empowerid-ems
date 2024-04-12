import { Button, Label, TextInput } from "flowbite-react";
import { BACKEND_URL } from "../../constants";
import { Employee } from "../employees/Employees";
import { Dispatch, SetStateAction } from "react";

const searchEmployees = async (name?: string, email?: string, department?: string): Promise<Employee[]> => {
    // Construct the query string based on the non-empty parameters
    let queryString = '';
    if (name) queryString += `&name=${encodeURIComponent(name)}`;
    if (email) queryString += `&email=${encodeURIComponent(email)}`;
    if (department) queryString += `&department=${encodeURIComponent(department)}`;

    const response = await fetch(`${BACKEND_URL}/api/employees/search?${queryString}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (!response.ok) {
        console.log('Failed to fetch employees');
    }

    const responseData = await response.json();
    const employees: Employee[] = responseData.map((item: any) => ({
        id: item.id,
        name: item.name,
        email: item.email,
        dateOfBirth: item.dateOfBirth,
        department: item.department
    }));

    return employees.reverse();
};

const SearchEmployees = ({ setEmployees }: {
    setEmployees: Dispatch<SetStateAction<Employee[] | undefined>>
}) => {
    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        const target = event.currentTarget;
        const formData = new FormData(target);

        const name = formData.get('emp-name') as string;
        const email = formData.get('email') as string;
        const department = formData.get('department') as string;

        try {
            const fetchedEmployees = await searchEmployees(name ?? null, email ?? null, department ?? null);
            setEmployees(fetchedEmployees);
        } catch (error) {
            console.error('Error fetching employees:', error);
        }
    }

    return (
        <div className="search flex-grow-0 flex-shrink-0 py-4">
            <form className="gap-4 grid grid-cols-1 md:grid-cols-4" onSubmit={handleSubmit}>
                <div>
                    <div className="mb-2">
                        <Label htmlFor="emp-name" value="Name" />
                    </div>
                    <TextInput id="emp-name" name="emp-name" placeholder="Enter Employee Name here" />
                </div>
                <div>
                    <div className="mb-2">
                        <Label htmlFor="email" value="Email" />
                    </div>
                    <TextInput id="email" name="email" placeholder="name@domain.com" type="email" />
                </div>
                <div>
                    <div className="mb-2">
                        <Label htmlFor="department" value="Department" />
                    </div>
                    <TextInput id="department" name="department" placeholder="Enter Department here" />
                </div>
                <div className="content-end">
                    <Button type="submit">Search</Button>
                </div>
            </form>
        </div>
    )
}

export default SearchEmployees;