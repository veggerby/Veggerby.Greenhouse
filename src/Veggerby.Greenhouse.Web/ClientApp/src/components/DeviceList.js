import { Card, Button } from 'react-bootstrap';
import React from 'react';
import Grid from './Grid';

export const DeviceList = ({ devices, selectDevice, selectedDevice }) => devices && devices.length ?
    (
        <Grid cols={2}>
            {devices.map(device =>
                <Card key={device.id} border={selectedDevice && selectedDevice(device) ? "primary" : null} bg={(!device.enabled ? "light" : null)} text={(!device.enabled ? "muted" : null)}>
                    <Card.Body>
                        <Card.Title>{device.name}</Card.Title>
                        <Card.Text>
                            {device.id}
                        </Card.Text>
                        {selectDevice ?
                            <Button onClick={() => selectDevice(device)}>Select</Button>
                        : null}
                    </Card.Body>
                </Card>
            )}
        </Grid>
    )
    : 'No devices';