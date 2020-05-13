import { ListGroup } from 'react-bootstrap';
import React from 'react';

export const DeviceListSmall = ({ devices, selectDevice, selectedDevice }) => devices && devices.length ?
    (
        <div style={{ paddingTop: '10px' }}>
            <h4>Devices</h4>
            <ListGroup>
                {devices.map(device =>
                    <ListGroup.Item key={device.id} action onClick={() => selectDevice(device)} active={selectedDevice(device)}>
                        <small>{device.name}</small>
                    </ListGroup.Item>
                )}
            </ListGroup>
        </div>
    )
    : 'No devices';