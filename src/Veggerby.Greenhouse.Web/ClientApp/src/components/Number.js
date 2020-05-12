import React from 'react';
import { OverlayTrigger, Tooltip } from 'react-bootstrap';

const Number = ({ value, defaultValue = null, decimals = null, maxDecimals = null, minDecimals = null, percent = false, group = false, suffix = null }) => {
        if (!value) {
            if (!defaultValue) {
                return null;
            }

            return (<span>{defaultValue}</span>);
        }

        let format = {
            minimumFractionDigits: minDecimals || decimals,
            maximumFractionDigits: maxDecimals || decimals
        };

        if (percent) {
            format = { ...format, style: 'percent' };
        }

        let mainValue = value;
        let valueSuffix = null;

        if (group) {
            if (mainValue > 1E9) {
                mainValue = mainValue / 1E9;
                valueSuffix = 'b';
            } else if (mainValue > 1E6) {
                mainValue = mainValue / 1E6;
                valueSuffix = 'm';
            } else if (mainValue > 1E3) {
                mainValue = mainValue / 1E3;
                valueSuffix = 'K';
            }

            if (mainValue < 10) {
                format.maximumFractionDigits = 2;
            }
            else if (mainValue < 100) {
                format.maximumFractionDigits = 1;
            }
            else {
                format.maximumFractionDigits = 0;
            }
        }

        let number = (<span>{mainValue.toLocaleString(navigator.language, format)}{valueSuffix}{suffix}</span>);

        if (mainValue !== value) {
            let actual = (<span>{value.toLocaleString(navigator.language, format)}{suffix}</span>);

            const tooltip = (
                <Tooltip id="number-tooltop">
                    {actual}
                </Tooltip>
            );

            return (
              <OverlayTrigger placement="bottom" overlay={tooltip}>
                {number}
              </OverlayTrigger>
            );
        }

        return number;
    };

export default Number;