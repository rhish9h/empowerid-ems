import { Button, Modal } from "flowbite-react";
import { Dispatch, SetStateAction } from "react";
import { HiOutlineExclamationCircle } from "react-icons/hi";
import { BACKEND_URL } from "../../constants";
import { Employee } from "../employees/Employees";
import { getAllEmployees } from "../../App";

const DeleteEmployee = ({ openModal, setOpenModal, employee, setEmployees }: {
    openModal: boolean,
    setOpenModal: Dispatch<SetStateAction<boolean>>,
    employee: { id: number; name: string; email: string; dateOfBirth: string; department: string; } | undefined,
    setEmployees: Dispatch<SetStateAction<Employee[] | undefined>>
}) => {
    const handleDelete = async () => {
        try {
            const response = await fetch(`${BACKEND_URL}/api/employees/${employee?.id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                console.error('Failed to delete employee');
                return;
            }

            console.log('Employee deleted successfully');
        } catch (error) {
            console.error('Error:', error);
        }
        const newEmployees = await getAllEmployees();
        setEmployees(newEmployees);
        setOpenModal(false);
    }

    return (
        <>
            <Modal show={openModal} size="md" onClose={() => setOpenModal(false)} popup>
                <Modal.Header />
                <Modal.Body>
                    <div className="text-center">
                        <HiOutlineExclamationCircle className="mx-auto mb-4 h-14 w-14 text-gray-400 dark:text-gray-200" />
                        <h3 className="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">
                            Are you sure you want to erase this employee's data?
                        </h3>
                        <div className="flex justify-center gap-4">
                            <Button color="failure" onClick={handleDelete}>
                                {"Yes, I'm sure"}
                            </Button>
                            <Button color="gray" onClick={() => setOpenModal(false)}>
                                No, cancel
                            </Button>
                        </div>
                    </div>
                </Modal.Body>
            </Modal>
        </>
    );
}

export default DeleteEmployee;