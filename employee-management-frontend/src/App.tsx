import MyNavbar from "./components/navbar/MyNavbar";
import AddEmployee from "./components/addEmployee/AddEmployee";
import { Card } from "flowbite-react";

function App() {
  return (
    <main className="min-h-screen gap-2 dark:bg-gray-800 flex flex-col">
      <MyNavbar />
      <div className="flex flex-grow flex-col sm:flex-row">
        <div className="sm:w-1/2 md:w-1/4 w-full px-4 pb-4 flex flex-col">
          <AddEmployee />
        </div>
        <div className="sm:w-1/2 md:w-3/4 w-full px-4 pb-4 flex flex-col">
          <Card className="h-full">
            {/* Content for the second card */}
          </Card>
        </div>
      </div>
      
    </main>
  );
}

export default App;
