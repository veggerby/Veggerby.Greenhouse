import { LineChart, Line, XAxis, YAxis, Tooltip, CartesianGrid } from 'recharts';
import { Container, Row, Col, Tabs, Tab } from 'react-bootstrap';
import React, { Component } from 'react';
import moment from 'moment';
import Moment from 'react-moment';

import Time from './Time';
import Number from './Number';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { properties: [], devices: [], measurements: null, selectedDevice: null, selectedProperty: null, loading: true };
    }

    componentDidMount() {
        this.devices();
        this.properties();
        this.measurements(this.state.selectedDevice, this.state.selectedProperty);
    }

    selectDevice(device) {
        this.setState({ selectedDevice: device });
        this.measurements(device, this.state.selectedProperty);
    }

    selectProperty(property) {
        this.setState({ selectedProperty: property });
        this.measurements(this.state.selectedDevice, property);
    }

    renderProperties(properties) {
        if (!properties) {
            return "No properties";
        }

        var selectedId = this.state.selectedProperty ? this.state.selectedProperty.id : null;

        return (
            <div>
                <h2>Properties</h2>
                <table className='table table-striped table-hover' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Unit</th>
                            <th>Tolerance</th>
                            <th>Decimals</th>
                        </tr>
                    </thead>
                    <tbody>
                        {properties.map(property =>
                            <tr key={property.id} onClick={() => this.selectProperty(property)} className={property.id == selectedId ? "table-primary" : ""}>
                                <td>{property.id}</td>
                                <td>{property.name}</td>
                                <td>{property.unit}</td>
                                <td>{property.tolerance}</td>
                                <td>{property.decimals}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    renderDevices(devices) {
        if (!devices) {
            return "No devices";
        }

        var selectedId = this.state.selectedDevice ? this.state.selectedDevice.id : null;

        return (
            <div>
                <h2>Devices</h2>
                <table className='table table-striped table-hover' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                        </tr>
                    </thead>
                    <tbody>
                        {devices.map(device =>
                            <tr key={device.id} onClick={() => this.selectDevice(device)} className={device.id == selectedId ? "table-primary" : ""}>
                                <td>{device.id}</td>
                                <td>{device.name}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    format(value, property) {
        return value.toLocaleString(navigator.language, { minimumFractionDigits: property.decimals, maximumFractionDigits: property.decimals }) + property.unit;
    }

    mapMeasurements(measurements) {
        var result = [];
        measurements.forEach(measurement => {
            if (measurement.signalCount == 1) {
                result.push({
                    time: moment(measurement.startTime).unix(),
                    value: measurement.averageValue
                });
            }
            else {
                result.push({
                    time: moment(measurement.startTime).unix(),
                    value: measurement.averageValue
                });

                result.push({
                    time: moment(measurement.endTime).unix(),
                    value: measurement.averageValue
                });
            }
        });

        return result;
    }

    renderChart(measurements) {
        if (!measurements) {
            return;
        }

        return (
            <div>
                <h2>{measurements.property.name} from {measurements.device.name}</h2>
                <LineChart
                    width={1000}
                    height={400}
                    data={this.mapMeasurements(measurements.measurements)}
                    margin={{ top: 5, right: 20, left: 10, bottom: 5 }}
                >
                    <XAxis dataKey="time" label="Time" tickFormatter={value => moment.unix(value).fromNow()} domain={['dataMin', 'dataMax']} type='number' />
                    <YAxis label={measurements.property.name} domain={['auto', 'auto']} />

                    <Tooltip
                        wrapperStyle={{
                            borderColor: 'white',
                            boxShadow: '2px 2px 3px 0px rgb(204, 204, 204)',
                        }}
                        contentStyle={{ backgroundColor: 'rgba(255, 255, 255, 0.8)' }}
                        labelStyle={{ fontWeight: 'bold', color: '#666666' }}
                        labelFormatter={v => moment.unix(v).toString() + " (" + moment.unix(v).fromNow() + ")"}
                        formatter={v => this.format(v, measurements.property)}
                    />

                    <CartesianGrid stroke="#f5f5f5" vertical={false} />
                    <Line type="monotone" dataKey="value" stroke="#ff7300" dot={false} />
                </LineChart>
            </div>
        );
    }

    renderMeasurements(measurements) {
        if (!measurements) {
            return;
        }

        return (
            <div>
                <h2>{measurements.property.name} from {measurements.device.name}</h2>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Start time</th>
                            <th>End time</th>
                            <th>Duration</th>
                            <th>{measurements.property.name} ({measurements.property.unit})</th>
                            <th>Count</th>
                        </tr>
                    </thead>
                    <tbody>
                        {measurements.measurements.map(measurement =>
                            <tr key={measurement.startTime}>
                                <td><Time time={measurement.startTime} /></td>
                                <td><Time time={measurement.endTime} /></td>
                                <td>
                                    <Moment duration={measurement.startTime}
                                        date={measurement.endTime}
                                    />
                                </td>
                                <td><Number value={measurement.averageValue} decimals={measurements.property.decimals} /></td>
                                <td>{measurement.signalCount}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    render() {
        return (
            <Container>
                <Row>
                    <Col xs={6} md={12}>
                        <h1>Measurements</h1>
                        <p>Select a property and a device to show measurements</p>
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <Tabs id="measurementsTab" defaultActiveKey={1}>
                            <Tab eventKey={1} title="Properties">
                                {this.renderProperties(this.state.properties)}
                            </Tab>
                            <Tab eventKey={2} title="Devices">
                                {this.renderDevices(this.state.devices)}
                            </Tab>
                            <Tab eventKey={3} title="Chart">
                                {this.renderChart(this.state.measurements)}
                            </Tab>
                            <Tab eventKey={4} title="Table">
                                {this.renderMeasurements(this.state.measurements)}
                            </Tab>
                        </Tabs>
                    </Col>
                </Row>
            </Container>
        );
    }

    async properties() {
        const response = await fetch('/api/properties');
        const data = await response.json();
        this.setState({ properties: data });
    }

    async devices() {
        const response = await fetch('/api/devices');
        const data = await response.json();
        this.setState({ devices: data });

        if (data.length === 1) {
            this.setState({ selectedDevice: data[0] });
        }
    }

    async measurements(device, property) {
        if (!(device && property)) {
            return;
        }

        var params = {
            d: device.id,
            p: property.id
        };

        var esc = encodeURIComponent;
        var query = Object.keys(params)
            .map(k => esc(k) + '=' + esc(params[k]))
            .join('&');

        const response = await fetch("/api/measurements?" + query);

        var data = null;

        console.log(response);

        if (response.status === 200) {
            data = await response.json();
        }

        this.setState({ measurements: data, loading: false });
    }
}
