import { ListGroup } from 'react-bootstrap';
import React from 'react';

export const SensorListSmall = ({ sensors, selectSensor, selectedSensor }) => sensors && sensors.length ?
    (
        <div style={{ paddingTop: '10px' }}>
            <h4>Sensors</h4>
            <ListGroup>
                {sensors.map(sensor =>
                    <ListGroup.Item key={sensor.id} action onClick={() => selectSensor(sensor)} active={selectedSensor(sensor)}>
                        <small>{sensor.name} on {sensor.device.name}</small>
                    </ListGroup.Item>
                )}
            </ListGroup>
        </div>
    )
    : 'No devices';