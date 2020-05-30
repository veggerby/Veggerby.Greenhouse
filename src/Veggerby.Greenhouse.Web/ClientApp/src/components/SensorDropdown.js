import React from 'react';
import { DropdownButton, Dropdown } from 'react-bootstrap';

export const SensorDropdown = ({ sensors, selectSensor, selectedSensor }) => sensors && sensors.length ?
    (
        <DropdownButton title="Select sensor(s)">
            {sensors.map(sensor =>
                (
                    <Dropdown.Item key={sensor.key} onClick={() => selectSensor(sensor)} active={selectedSensor(sensor)}>
                        {sensor.name} on {sensor.device.name}
                    </Dropdown.Item>
                ))
            }
        </DropdownButton>
    )
    : 'No sensors';