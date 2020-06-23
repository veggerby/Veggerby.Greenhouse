import React from 'react';
import { Navbar, Nav, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { useAuth0 } from '../react-auth0-spa';

import dashboards from "../dashboards.json";

export const NavMenu = () => {
    const { isAuthenticated, loginWithRedirect, logout } = useAuth0();

    return (
        <header>
            <Navbar bg="light" expand="lg">
                <Navbar.Brand href="/">Veggerby Greenhouse</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="mr-auto">
                        <Nav.Link as={Link} to="/">Home</Nav.Link>
                        {dashboards.map(dashboard => <Nav.Link key={dashboard.route} as={Link} to={`/dashboard/${dashboard.route}`}>{dashboard.title}</Nav.Link>)}
                        <Nav.Link as={Link} to="/properties">Properties</Nav.Link>
                        <Nav.Link as={Link} to="/devices">Devices</Nav.Link>
                        <Nav.Link as={Link} to="/sensors">Sensors</Nav.Link>
                        <Nav.Link as={Link} to="/measurements">Measurements</Nav.Link>
                    </Nav>
                    {isAuthenticated && (
                        <Nav>
                            <Button size="sml" onClick={() => logout({})}>
                                Log out
                            </Button>
                        </Nav>
                      )}

                    {!isAuthenticated && (
                        <Nav>
                            <Button size="sml" onClick={() => loginWithRedirect({})}>
                                Log in
                            </Button>
                        </Nav>
                    )}
                </Navbar.Collapse>
            </Navbar>
        </header>
    );
}
