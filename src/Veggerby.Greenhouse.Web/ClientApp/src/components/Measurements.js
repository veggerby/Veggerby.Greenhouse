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
    }

    selectProperty(property) {
        this.setState({ selectedProperty: property });
        this.switchTab(property);
    }

    switchTab(property, selectedSensors) {
        if ((property || this.state.selectedProperty) && (selectedSensors || this.state.selectedSensors.length)) {
            this.measurements(selectedSensors || this.state.selectedSensors, property || this.state.selectedProperty);
            this.selectTab("chart");
        }
        else if (this.state.selectedSensors.length) {
            this.selectTab("properties");
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
                    <Col xs={12} md={12}>
                        <h1>Measurements</h1>
                        {
                            this.state.selectedProperty ?
                                <p>Showing {this.state.selectedProperty.name}</p> :
                                <p>Select a property and a device to show measurements</p>
                        }
                    </Col>
                </Row>
                <Row>
                    <Col xs={12} md={12}>
                        <Tabs id="measurementsTab" activeKey={this.state.activeTab} onSelect={(k) => this.selectTab(k)} variant="pills">
                            <Tab eventKey="chart" title="Chart">
                                <MeasurementChart measurements={this.state.measurements} />
                                <PropertyListSmall properties={this.state.properties} selectProperty={this.selectProperty} selectedProperty={this.isSelectedProperty} />
                                <SensorListSmall sensors={this.state.sensors} selectSensor={this.toggleSensor} selectedSensor={this.isSelectedSensor} />
                            </Tab>
                            <Tab eventKey="properties" title="Properties">
                                <PropertyList properties={this.state.properties} selectProperty={this.selectProperty} selectedProperty={this.isSelectedProperty} />
                            </Tab>
                            <Tab eventKey="sensors" title="Sensors">
                                <SensorList sensors={this.state.sensors} selectSensor={this.toggleSensor} selectedSensor={this.isSelectedSensor} />
                            </Tab>
                            <Tab eventKey="devices" title="Devices">
                                <DeviceList devices={this.state.devices} selectDevice={this.toggleDevice} selectedDevice={this.isSelectedDevice} />
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
        this.switchTab();
    }

    async sensors() {
        const response = await fetch('/api/sensors');
        const data = await response.json();

        this.setState({ sensors: data, loading: false, selectedSensors: data.map(s => s.key) });
        this.switchTab();
    }

    async properties() {
        const response = await fetch('/api/properties');
        const data = await response.json();

        var property = null;

        if (data && data.length > 0) {
            property = data[0];
        }

        this.setState({ properties: data, selectedProperty: property, loading: false });

        this.switchTab();
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
