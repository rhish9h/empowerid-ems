import MyNavbar from "./components/navbar/MyNavbar";
import AddEmployee from "./components/addEmployee/AddEmployee";
import Employees, { Employee } from "./components/employees/Employees";
import { useState, useEffect } from "react";
import { BACKEND_URL } from "./constants";

export const getAllEmployees = async (): Promise<Employee[]> => {
  const response = await fetch(`${BACKEND_URL}/api/employees`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json'
    }
  });

  if (!response?.ok) {
    console.error('Failed to fetch employees');
    return [];
  }

  const responseData = await response.json();
  const employees: Employee[] = responseData.map((item: any) => ({
    id: item.id,
    name: item.name,
    email: item.email,
    dateOfBirth: item.dateOfBirth,
    department: item.department
  }));

  // Sort employees based on id in descending order
  const sortedEmployees = employees.sort((a, b) => b.id - a.id);
  return sortedEmployees;
};


const App = () => {
  const [employees, setEmployees] = useState<Employee[]>();

  useEffect(() => {
    const fetchEmployees = async () => {
      try {
        const fetchedEmployees = await getAllEmployees();
        setEmployees(fetchedEmployees);
      } catch (error) {
        console.error('Error fetching employees:', error);
      }
    };

    fetchEmployees();
  }, []);

  return (
    <main className="min-h-screen gap-2 dark:bg-gray-800 flex flex-col">
      <MyNavbar />
      <div className="flex flex-grow flex-col sm:flex-row">
        <div className="sm:w-1/2 md:w-1/4 w-full px-4 pb-4 flex flex-col">
          <AddEmployee setEmployees={setEmployees} />
        </div>
        <div className="sm:w-1/2 md:w-3/4 w-full px-4 pb-4 flex flex-col">
          <Employees employees={employees} setEmployees={setEmployees} />
        </div>
      </div>

    </main>
  );
}

export default App;
