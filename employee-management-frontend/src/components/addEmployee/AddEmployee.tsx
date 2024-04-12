import { Button, Card, Label, TextInput } from "flowbite-react";

const AddEmployee = () => {
    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        console.log(event.currentTarget);
        const target = event.currentTarget;
        const formData = new FormData(target);
        const name = formData.get('emp-name') as string;
        const email = formData.get('email') as string;
        const date = formData.get('dob') as string;
        const department = formData.get('department') as string;
        console.log(name, email, date, department);
    }

    return (
        <Card className="flex-grow">
            <h5 className="text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
                Add Employee
            </h5>
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
                    <TextInput id="email" name="email" placeholder="name@domain.com" type="email" required />
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
        </Card>
    );
}

export default AddEmployee;