import { Button, Label, Modal, TextInput } from "flowbite-react";
import { Dispatch, SetStateAction, useState, useEffect } from "react";
import { Employee, parseDate } from "../employees/Employees";
import { BACKEND_URL } from "../../constants";
import { getAllEmployees } from "../../App";

const EditEmployee = ({ openModal, setOpenModal, employee, setEmployees }: {
    openModal: boolean,
    setOpenModal: Dispatch<SetStateAction<boolean>>,
    employee: { id: number; name: string; email: string; dateOfBirth: string; department: string; } | undefined,
    setEmployees: Dispatch<SetStateAction<Employee[] | undefined>>
}) => {
    const [name, setName] = useState(employee?.name ?? '');
    const [email, setEmail] = useState(employee?.email ?? '');
    const date = parseDate(employee?.dateOfBirth);
    const [dateOfBirth, setDateOfBirth] = useState(date);
    const [department, setDepartment] = useState(employee?.department ?? '');
    // console.log(employee, email, employee?.email);

    // Update state when the employee prop changes
    useEffect(() => {
        if (employee) {
            setName(employee.name);
            setEmail(employee.email);
            setDateOfBirth(parseDate(employee.dateOfBirth));
            setDepartment(employee.department);
        }
    }, [employee]);

    // Reset form state when the modal is closed
    useEffect(() => {
        if (!openModal) {
            setName('');
            setEmail('');
            setDateOfBirth('');
            setDepartment('');
        }
    }, [openModal]);

    const onCloseModal = () => {
        setOpenModal(false);
    }

    const handleSubmit = async () => {
        try {
            const updatedEmployee = {
                name,
                email,
                dateOfBirth,
                department
            }

            const response = await fetch(`${BACKEND_URL}/api/employees/${employee?.id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(updatedEmployee)
            });

            if (!response.ok) {
                console.error('Failed to edit employee');
                return;
            }

            console.log('Employee edited successfully');
            const newEmployees = await getAllEmployees();
            setEmployees(newEmployees);
            setOpenModal(false);
        } catch (error) {
            console.error('Error editing employee:', error);
        }
    }

    return (
        <>
            <Modal show={openModal} size="md" onClose={onCloseModal} popup>
                <Modal.Header />
                <Modal.Body>
                    <div className="space-y-6">
                        <h3 className="text-xl font-medium text-gray-900 dark:text-white">Edit Employee</h3>
                        <form className="flex flex-col gap-4" onSubmit={handleSubmit}>
                            <div>
                                <div className="mb-2 block">
                                    <Label htmlFor="emp-name" value="Name" />
                                </div>
                                <TextInput id="emp-name" name="emp-name" placeholder="Enter Employee Name here" required value={name} onChange={(event) => setName(event.target.value)} />
                            </div>
                            <div>
                                <div className="mb-2 block">
                                    <Label htmlFor="email" value="Email" />
                                </div>
                                <TextInput id="email" name="email" placeholder="name@domain.com" type="email" value={email} onChange={(event) => setEmail(event.target.value)} required />
                            </div>
                            <div>
                                <div className="mb-2 block">
                                    <Label htmlFor="dob" value="Date of birth" />
                                </div>
                                <TextInput id="dob" name="dob" type="date" required value={dateOfBirth} onChange={(event) => setDateOfBirth(event.target.value)} />
                            </div>
                            <div>
                                <div className="mb-2 block">
                                    <Label htmlFor="department" value="Department" />
                                </div>
                                <TextInput id="department" name="department" placeholder="Enter Department here" required value={department} onChange={(event) => setDepartment(event.target.value)} />
                            </div>
                            <Button type="submit">Submit</Button>
                        </form>
                    </div>
                </Modal.Body>
            </Modal>
        </>
    );
}

export default EditEmployee;