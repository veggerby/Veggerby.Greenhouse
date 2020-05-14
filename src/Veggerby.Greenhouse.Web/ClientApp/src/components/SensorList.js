import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import React from 'react';
import Grid from './Grid';

export const SensorList = ({ sensors, selectSensor, selectedSensor }) => sensors && sensors.length ?
    (
        <Container>
            <Row>
                <Col xs={6} md={12}>
                    <h3>Sensors</h3>
                    <p>Select a sensor</p>
                </Col>
            </Row>
            <Row>
                <Col>
                    <Grid>
                        {sensors.map(sensor =>
                            <Card key={sensor.id} border={selectedSensor(sensor) ? "primary" : null}>
                                <Card.Body>
                                    <Card.Title>{sensor.name}</Card.Title>
                                    <Card.Text>
                                        {sensor.id} on {sensor.device.name}
                                    </Card.Text>
                                    <Button onClick={() => selectSensor(sensor)}>Select</Button>
                                </Card.Body>
                            </Card>
                        )}
                    </Grid>
                </Col>
            </Row>
        </Container>
        )
        : 'No devices';