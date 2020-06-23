import { Table, Card, Button } from 'react-bootstrap';
import React from 'react';

import Grid from './Grid';

export const PropertyList = ({ properties, selectProperty, selectedProperty }) => properties && properties.length ?
    (
        <Grid cols={2}>
            {properties.map(property =>
                <Card key={property.id} border={selectedProperty && selectedProperty(property) ? "primary" : null}>
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
                                {property.domain ? (
                                    <tr>
                                        <td>Domain</td>
                                        <td>
                                            {property.domain.name}
                                            <ul>
                                                {property.domain.values.map(v => <li key={v.id}>{v.name}</li>)}
                                            </ul>
                                        </td>
                                    </tr>
                                ) : null}
                            </tbody>
                        </Table>
                        {selectProperty ?
                            <Button onClick={() => selectProperty(property)}>Select</Button>
                            : null}
                    </Card.Body>
                </Card>
            )}
        </Grid>
    ) : "No properties";
