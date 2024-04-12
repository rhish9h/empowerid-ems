import { Card, Table } from "flowbite-react";
import { Dispatch, SetStateAction } from "react";
import SearchEmployees from "../searchEmployees/SearchEmployees";

const Employees = ({ employees, setEmployees }: {
    employees: { id: number; name: string; email: string; dateOfBirth: string; department: string; }[],
    setEmployees: Dispatch<SetStateAction<{ id: number; name: string; email: string; dateOfBirth: string; department: string; }[]>>
}) => {
    const setEmplo = setEmployees;
    const parseDate = (timestamp: string) => {
        const date = new Date(timestamp);
        // Get the date part in YYYY-MM-DD format
        const formattedDate = date.toISOString().split('T')[0];
        return formattedDate;
    }

    return (
        <Card className="h-full">
            <SearchEmployees />
            <div className="emp-list flex-grow">
                <div className="overflow-x-auto">
                    <Table hoverable>
                        <Table.Head>
                            <Table.HeadCell>Id</Table.HeadCell>
                            <Table.HeadCell>Name</Table.HeadCell>
                            <Table.HeadCell>Email</Table.HeadCell>
                            <Table.HeadCell>Date of Birth</Table.HeadCell>
                            <Table.HeadCell>Department</Table.HeadCell>
                            <Table.HeadCell>
                                <span className="sr-only">Edit</span>
                            </Table.HeadCell>
                        </Table.Head>

                        <Table.Body className="divide-y">
                            {employees.map((emp: any) => (
                                <Table.Row className="bg-white dark:border-gray-700 dark:bg-gray-800" key={emp.id}>
                                    <Table.Cell>{emp.id}</Table.Cell>
                                    <Table.Cell className="whitespace-nowrap font-medium text-gray-900 dark:text-white">
                                        {emp.name}
                                    </Table.Cell>
                                    <Table.Cell>{emp.email}</Table.Cell>
                                    <Table.Cell>{parseDate(emp.dateOfBirth)}</Table.Cell>
                                    <Table.Cell>
                                        <a href="#" className="font-medium text-cyan-600 hover:underline dark:text-cyan-500">
                                            Edit
                                        </a>
                                        <a href="#" className="font-medium text-cyan-600 hover:underline dark:text-cyan-500 ml-4">
                                            Delete
                                        </a>
                                    </Table.Cell>
                                </Table.Row>
                            ))}
                        </Table.Body>
                    </Table>
                </div>
            </div>
        </Card>
    )
}

export default Employees;