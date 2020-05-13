import { LineChart, Line, XAxis, YAxis, Tooltip, CartesianGrid } from 'recharts';
import React from 'react';
import moment from 'moment';
import { Container } from 'react-bootstrap';

let mapMeasurements = (measurements) => {
    var result = [];

    measurements.measurements.forEach(measurement => {
        if (measurement.signalCount === 1) {
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
};

const colors = [
    '#ff7300',
    '#387908',
    '#38abc8'
];

let format = (value, property) => value.toLocaleString(navigator.language, { minimumFractionDigits: property.decimals, maximumFractionDigits: property.decimals }) + " " + property.unit;

export const MeasurementChart = ({ measurements }) => measurements && measurements.length ?
    (
        <Container>
            <h2>{measurements[0].property.name}</h2>
            <LineChart
                width={840}
                height={400}
                margin={{ top: 5, right: 20, left: 10, bottom: 5 }}
            >
                <XAxis dataKey="time" label={{ value: "Time", position: "insideBottomRight", dy: 10 }} tickFormatter={value => moment.unix(value).fromNow()} domain={['dataMin', 'dataMax']} type='number' />
                <YAxis dataKey="value" label={{ value: measurements[0].property.name + " (" + measurements[0].property.unit + ")", position: "insideLeft", angle: -90, dx: -5 }} domain={['auto', 'auto']} />

                <Tooltip
                    wrapperStyle={{
                        borderColor: 'white',
                        boxShadow: '2px 2px 3px 0px rgb(204, 204, 204)',
                    }}
                    contentStyle={{ backgroundColor: 'rgba(255, 255, 255, 0.8)' }}
                    labelStyle={{ fontWeight: 'bold', color: '#666666' }}
                    labelFormatter={v => moment.unix(v).toString() + " (" + moment.unix(v).fromNow() + ")"}
                    formatter={v => format(v, measurements[0].property)}
                />

                <CartesianGrid stroke="#f5f5f5" vertical={false} />
                {measurements.map((measurement, ixc) =>
                    <Line key={measurement.device.id} type="monotone" data={mapMeasurements(measurement)} dataKey="value" stroke={colors[ixc % colors.length]} dot={false} name={measurement.device.id} />
                )}
            </LineChart>
        </Container>
    ) : "No measurements";
