import React, { Component } from 'react';
import { Container, Row, Col } from 'react-bootstrap';

class Grid extends Component {
    render() {
        const { cols = 4 } = this.props;

        const gwidth = 12 / cols;

        let cells = this.props.children.map((child, ixc) => (<Col key={ixc} sm={6} md={gwidth} style={{ padding: "5px" }}>{child}</Col>));

        let rows = cells.map((cell, ixc) => ixc % cols === 0 ? cells.slice(ixc, ixc + cols) : null).filter(x => x);

        return (
            <Container >
                {rows.map((row, ixc) => (<Row key={ixc}>{row}</Row>))}
            </Container>
        );
    }
}

export default Grid;