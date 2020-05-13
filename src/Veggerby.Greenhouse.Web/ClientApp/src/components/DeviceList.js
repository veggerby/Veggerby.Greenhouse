import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import React from 'react';
import Grid from './Grid';

export const DeviceList = ({ devices, selectDevice, selectedDevice }) => devices && devices.length ?
    (
        <Container>
            <Row>
                <Col xs={6} md={12}>
                    <h3>Devices</h3>
                    <p>Select a device</p>
                </Col>
            </Row>
            <Row>
                <Col>
                    <Grid>
                        {devices.map(device =>
                            <Card key={device.id} border={selectedDevice(device) ? "primary" : null}>
                                <Card.Body>
                                    <Card.Title>{device.name}</Card.Title>
                                    <Card.Text>
                                        {device.id}
                                    </Card.Text>
                                    <Button onClick={() => selectDevice(device)}>Select</Button>
                                </Card.Body>
                            </Card>
                        )}
                    </Grid>
                </Col>
            </Row>
        </Container>
        )
        : 'No devices';