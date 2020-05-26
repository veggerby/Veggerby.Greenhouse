import React, { Component } from 'react';
import { Container } from 'react-bootstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
    render() {
        return (
            <>
                <NavMenu />
                <Container>
                    {this.props.children}
                </Container>
            </>
        );
    }
}
