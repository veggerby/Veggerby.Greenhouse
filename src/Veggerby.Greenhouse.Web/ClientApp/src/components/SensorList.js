import { Card, Button } from 'react-bootstrap';
import React from 'react';
import Grid from './Grid';

export const SensorList = ({ sensors, selectSensor, selectedSensor }) => sensors && sensors.length ?
    (
        <Grid cols={2}>
            {sensors.map(sensor =>
                <Card key={sensor.id} border={selectedSensor && selectedSensor(sensor) ? "primary" : null} bg={(!sensor.enabled || !sensor.device.enabled ? "light" : null)} text={(!sensor.enabled || !sensor.device.enabled ? "muted" : null)}>
                    <Card.Body>
                        <Card.Title>{sensor.name}</Card.Title>
                        <Card.Text>
                            {sensor.id} on {sensor.device.name}
                        </Card.Text>
                        {selectSensor ?
                            <Button onClick={() => selectSensor(sensor)}>Select</Button>
                        : null}
                    </Card.Body>
                </Card>
            )}
        </Grid>
    )
    : 'No sensors';