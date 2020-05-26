import React from 'react';
import { Navbar, Nav } from 'react-bootstrap';

export const NavMenu = () => {
    return (
        <header>
            <Navbar bg="light" expand="lg">
                <Navbar.Brand href="/">Veggerby Greenhouse</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="mr-auto">
                        <Nav.Link href="/">Home</Nav.Link>
                        <Nav.Link href="/measurements">Measurements</Nav.Link>
                    </Nav>
                </Navbar.Collapse>
            </Navbar>
        </header>
    );
}
