import { DarkThemeToggle } from "flowbite-react";
import { Navbar } from "flowbite-react";

const MyNavbar = () => {
    return (
        <Navbar fluid rounded>
            <Navbar.Brand href="#">
                <span className="self-center whitespace-nowrap text-xl font-semibold dark:text-white">Rhish</span>
            </Navbar.Brand>
            <div className="flex md:order-2">
                <DarkThemeToggle />
                <Navbar.Toggle />
            </div>
            <Navbar.Collapse>
                <Navbar.Link href="#" active>
                    Home
                </Navbar.Link>
                <Navbar.Link href="https://www.linkedin.com/in/rhishabh-hattarki/" target="_blank">LinkedIn</Navbar.Link>
                <Navbar.Link href="https://github.com/rhish9h" target="_blank">GitHub</Navbar.Link>
                <Navbar.Link href="https://rhish.in" target="_blank">Portfolio</Navbar.Link>
            </Navbar.Collapse>
        </Navbar>
    )
}

export default MyNavbar;