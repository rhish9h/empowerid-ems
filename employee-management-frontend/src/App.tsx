import MyNavbar from "./components/navbar/MyNavbar";
import AddEmployee from "./components/addEmployee/AddEmployee";
import Employees from "./components/employees/Employees";
import { useState } from "react";

function App() {
  const [employees, setEmployees] = useState([
    { id: 1, name: "string", email: "string", dateOfBirth: "2024-04-12T04:29:11.606Z", department: "string" },
    { id: 2, name: "string", email: "string", dateOfBirth: "2024-04-12T04:29:11.606Z", department: "string" }
  ]);

  return (
    <main className="min-h-screen gap-2 dark:bg-gray-800 flex flex-col">
      <MyNavbar />
      <div className="flex flex-grow flex-col sm:flex-row">
        <div className="sm:w-1/2 md:w-1/4 w-full px-4 pb-4 flex flex-col">
          <AddEmployee />
        </div>
        <div className="sm:w-1/2 md:w-3/4 w-full px-4 pb-4 flex flex-col">
          <Employees employees={employees} setEmployees={setEmployees} />
        </div>
      </div>

    </main>
  );
}

export default App;
