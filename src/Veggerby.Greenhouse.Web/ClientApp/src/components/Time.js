import React from 'react';
import { OverlayTrigger, Tooltip } from 'react-bootstrap';
import Moment from 'react-moment';
import 'moment-timezone';

const Time = ({ time, defaultValue = null }) => {
        if (!time) {
            if (!defaultValue) {
                return null;
            }

            return (<span>{defaultValue}</span>);
        }

        const tooltip = (
            <Tooltip id="time-tooltip">
                <Moment>{time}</Moment>
            </Tooltip>
        );

        return (
            <OverlayTrigger placement="bottom" overlay={tooltip}>
                <Moment fromNow>{time}</Moment>
            </OverlayTrigger>
        );
    };


export default Time;