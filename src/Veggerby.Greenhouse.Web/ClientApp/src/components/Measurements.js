import React, { Component } from 'react';
import { Container, Row, Col, Tabs, Tab } from 'react-bootstrap';

import { DeviceList } from './DeviceList';
import { PropertyList } from './PropertyList'
import { MeasurementChart } from './MeasurementChart';
import { MeasurementTable } from './MeasurementTable';
import { PropertyListSmall } from './PropertyListSmall';
import { SensorListSmall } from './SensorListSmall';
import { SensorList } from './SensorList';

export class Measurements extends Component {
    static displayName = Measurements.name;

    constructor(props) {
        super(props);
        this.state = {
            measurements: null,
            devices: [],
            sensors: [],
            properties: [],
            selectedDevices: [],
            selectedSensors: [],
            selectedProperty: null,
            loading: true,
            activeTab: "properties"
        };

        this.selectProperty = this.selectProperty.bind(this);

        this.isSelectedProperty = this.isSelectedProperty.bind(this);
        this.isSelectedDevice = this.isSelectedDevice.bind(this);
        this.isSelectedSensor = this.isSelectedSensor.bind(this);

        this.toggleDevice = this.toggleDevice.bind(this);
        this.toggleSensor = this.toggleSensor.bind(this);
    }

    componentDidMount() {
        this.properties();
        this.devices();
        this.sensors();
        this.measurements(this.state.selectedSensors, this.state.selectedProperty);
    }

    selectProperty(property) {
        this.setState({ selectedProperty: property });
        this.measurements(this.state.selectedSensors, property);

        if (this.state.selectedSensors.length) {
            this.selectTab("chart");
        }
        else {
            this.selectTab("sensors");
        }
    }

    toggleDevice(device) {
        var array = [...this.state.selectedDevices]; // make a separate copy of the array
        var index = array.indexOf(device.id)
        if (index !== -1) {
            array.splice(index, 1);
        }
        else  {
            array.push(device.id);
        }

        console.log(array);

        this.setState({selectedDevices: array});
    }

    toggleSensor(sensor) {
        var array = [...this.state.selectedSensors]; // make a separate copy of the array
        var index = array.indexOf(sensor.key)
        if (index !== -1) {
            array.splice(index, 1);
        }
        else  {
            array.push(sensor.key);
        }

        console.log(array);

        this.setState({selectedSensors: array});
        this.measurements(array, this.state.selectedProperty);

        if (this.state.selectedProperty) {
            this.selectTab("chart");
        }
        else {
            this.selectTab("properties");
        }
    }

    selectTab(tab) {
        this.setState({ activeTab: tab });
    }

    isSelectedProperty(property) {
        return this.state.selectedProperty && property.id === this.state.selectedProperty.id;
    }

    isSelectedDevice(device) {
        var index = this.state.selectedDevices.indexOf(device.id)
        return index !== -1;
        //return this.state.selectedDevice && device.id == this.state.selectedDevice.id;
    }

    isSelectedSensor(sensor) {
        var index = this.state.selectedSensors.indexOf(sensor.key)
        return index !== -1;
        //return this.state.selectedDevice && device.id == this.state.selectedDevice.id;
    }

    render() {
        return (
            <Container>
                <Row>
                    <Col xs={6} md={12}>
                        <h1>Measurements</h1>
                        {
                            this.state.selectedProperty ?
                                <p>Showing {this.state.selectedProperty.name}</p> :
                                <p>Select a property and a device to show measurements</p>
                        }
                    </Col>
                </Row>
                <Row>
                    <Col xs={6} md={12}>
                        <Tabs id="measurementsTab" activeKey={this.state.activeTab} onSelect={(k) => this.selectTab(k)}>
                            <Tab eventKey="properties" title="Properties">
                                <PropertyList properties={this.state.properties} selectProperty={this.selectProperty} selectedProperty={this.isSelectedProperty} />
                            </Tab>
                            <Tab eventKey="devices" title="Devices">
                                <DeviceList devices={this.state.devices} selectDevice={this.toggleDevice} selectedDevice={this.isSelectedDevice} />
                            </Tab>
                            <Tab eventKey="sensors" title="Sensors">
                                <SensorList sensors={this.state.sensors} selectSensor={this.toggleSensor} selectedSensor={this.isSelectedSensor} />
                            </Tab>
                            <Tab eventKey="chart" title="Chart">
                                <Container>
                                    <Row>
                                        <Col xs={6} md={10}>
                                            <MeasurementChart measurements={this.state.measurements} />
                                        </Col>
                                        <Col xs={6} md={2}>
                                            <PropertyListSmall properties={this.state.properties} selectProperty={this.selectProperty} selectedProperty={this.isSelectedProperty} />
                                            <SensorListSmall sensors={this.state.sensors} selectSensor={this.toggleSensor} selectedSensor={this.isSelectedSensor} />
                                        </Col>
                                    </Row>
                                </Container>
                            </Tab>
                            <Tab eventKey="table" title="Table">
                                <MeasurementTable measurements={this.state.measurements} />
                            </Tab>
                        </Tabs>
                    </Col>
                </Row>
            </Container>
        );
    }

    async devices() {
        const response = await fetch('/api/devices');
        const data = await response.json();

        this.setState({ devices: data, loading: false });
    }

    async sensors() {
        const response = await fetch('/api/sensors');
        const data = await response.json();

        this.setState({ sensors: data, loading: false, selectedSensors: data.map(s => s.key) });
    }

    async properties() {
        const response = await fetch('/api/properties');
        const data = await response.json();
        this.setState({ properties: data, loading: false });
    }

    async measurements(sensors, property) {
        if (!(sensors && property)) {
            return;
        }

        var params = sensors.map(s => { return { k: 's', v: s }});
        params.push({ k: 'p', v: property.id });

        var query = params
            .map(p => encodeURIComponent(p.k) + '=' + encodeURIComponent(p.v))
            .join('&');

        const response = await fetch("/api/measurements?" + query);

        var data = null;

        //console.log(response);

        if (response.status === 200) {
            data = await response.json();
        }

        this.setState({ measurements: data, loading: false });
    }
}
