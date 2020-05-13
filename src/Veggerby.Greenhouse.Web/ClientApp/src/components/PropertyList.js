import { Container, Row, Col, Table, Card, Button } from 'react-bootstrap';
import React from 'react';

import Grid from './Grid';

export const PropertyList = ({ properties, selectProperty, selectedProperty }) => properties && properties.length ?
    (
        <Container>
                <Row>
                    <Col xs={6} md={12}>
                        <h3>Properties</h3>
                        <p>Select a property</p>
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <Grid>
                            {properties.map(property =>
                                <Card key={property.id} border={selectedProperty(property) ? "primary" : null}>
                                    <Card.Body>
                                        <Card.Title>{property.name}</Card.Title>
                                        <Table borderless size="sm">
                                            <tbody>
                                            <tr>
                                                    <td>Id</td>
                                                    <td>{property.id}</td>
                                                </tr>
                                                <tr>
                                                    <td>Unit</td>
                                                    <td>{property.unit}</td>
                                                </tr>
                                                <tr>
                                                    <td>Tolerance</td>
                                                    <td>{property.tolerance}</td>
                                                </tr>
                                                <tr>
                                                    <td>Decimals</td>
                                                    <td>{property.decimals}</td>
                                                </tr>
                                            </tbody>
                                        </Table>
                                        <Button onClick={() => selectProperty(property)}>Select</Button>
                                    </Card.Body>
                                </Card>
                            )}
                        </Grid>
                    </Col>
                </Row>
            </Container>
    ) : "No properties";
