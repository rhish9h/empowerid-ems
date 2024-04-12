import { Button, Label, TextInput } from "flowbite-react";

const SearchEmployees = () => {
    return (
        <div className="search flex-grow-0 flex-shrink-0 py-4">
            <form className="gap-4 grid grid-cols-1 md:grid-cols-4">
                <div>
                    <div className="mb-2">
                        <Label htmlFor="emp-name" value="Name" />
                    </div>
                    <TextInput id="emp-name" name="emp-name" placeholder="Enter Employee Name here" required />
                </div>
                <div>
                    <div className="mb-2">
                        <Label htmlFor="email" value="Email" />
                    </div>
                    <TextInput id="email" name="email" placeholder="name@domain.com" type="email" required />
                </div>
                <div>
                    <div className="mb-2">
                        <Label htmlFor="department" value="Department" />
                    </div>
                    <TextInput id="department" name="department" placeholder="Enter Department here" required />
                </div>
                <div className="content-end">
                    <Button type="submit">Search</Button>
                </div>
            </form>
        </div>
    )
}

export default SearchEmployees;