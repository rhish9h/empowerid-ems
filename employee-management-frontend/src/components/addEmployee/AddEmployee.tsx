import { Button, Card } from "flowbite-react";

const AddEmployee = () => {
    return (
        <Card className="flex-grow">
            <h5 className="text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
                Add Employee
            </h5>
            <p className="font-normal text-gray-700 dark:text-gray-400">
                form here
            </p>
            <Button>
                Read more
            </Button>
        </Card>
    );
}

export default AddEmployee;