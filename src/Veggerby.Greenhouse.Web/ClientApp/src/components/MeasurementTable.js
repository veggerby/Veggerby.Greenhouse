import { Container, Table } from 'react-bootstrap';
import React from 'react';
import Moment from 'react-moment';

import Time from './Time';
import Number from './Number';


export const MeasurementTable = ({ measurements }) => measurements && measurements.length ?
    (
        <Container>
            {
                measurements.map(m =>
                    <div key={m.device.id}>
                        <h2>{m.property.name} from {m.device.name}</h2>
                            <Table striped aria-labelledby="tabelLabel">
                                <thead>
                                    <tr>
                                        <th>Start time</th>
                                        <th>End time</th>
                                        <th>Device</th>
                                        <th>Duration</th>
                                        <th>{m.property.name} ({m.property.unit})</th>
                                        <th>Count</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {m.measurements.map(measurement =>
                                        <tr key={measurement.startTime}>
                                            <td><Time time={measurement.startTime} /></td>
                                            <td><Time time={measurement.endTime} /></td>
                                            <td>{measurement.deviceId}</td>
                                            <td>
                                                <Moment duration={measurement.startTime}
                                                    date={measurement.endTime}
                                                />
                                            </td>
                                            <td><Number value={measurement.averageValue} decimals={m.property.decimals} /></td>
                                            <td>{measurement.signalCount}</td>
                                        </tr>
                                    )}
                                </tbody>
                            </Table>
                    </div>
                )
            }
        </Container>
    ) : "No measurements";
