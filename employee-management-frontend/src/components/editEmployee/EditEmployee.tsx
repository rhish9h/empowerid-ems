import { Button, Label, Modal, TextInput } from "flowbite-react";
import { Dispatch, SetStateAction, useState } from "react";

const EditEmployee = ({ openModal, setOpenModal, employee }: {
    openModal: boolean,
    setOpenModal: Dispatch<SetStateAction<boolean>>,
    employee: { id: number; name: string; email: string; dateOfBirth: string; department: string; } | undefined
}) => {
    const [email, setEmail] = useState(employee?.email ?? '');
    console.log(employee, email, employee?.email);

    const onCloseModal = () => {
        setOpenModal(false);
        setEmail('');
    }

    const handleSubmit = () => {}

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
                    <TextInput id="emp-name" name="emp-name" placeholder="Enter Employee Name here" required />
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
                    <TextInput id="dob" name="dob" type="date" required />
                </div>
                <div>
                    <div className="mb-2 block">
                        <Label htmlFor="department" value="Department" />
                    </div>
                    <TextInput id="department" name="department" placeholder="Enter Department here" required />
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