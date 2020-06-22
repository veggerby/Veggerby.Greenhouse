import React from 'react';
import { Button } from 'react-bootstrap';
import { useCookies } from 'react-cookie';

const COOKIE_NAME = 'auth0.is.authenticated';

export const Home = () => {
    // eslint-disable-next-line
    const [,, removeCookie] = useCookies(['COOKIE_NAME']);

    return (
        <>
            <h1>Veggerby Greenhouse</h1>

            <Button onClick={() => { removeCookie(COOKIE_NAME); window.location.reload(); }}>Remove Auth & Reload</Button>
        </>
    );
};