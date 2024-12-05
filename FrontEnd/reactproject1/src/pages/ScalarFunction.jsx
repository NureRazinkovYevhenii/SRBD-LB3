import React, { useState } from 'react';
import './css/ScalarFunction.css';

const GetAuthorPriceCountryCount = () => {
    const [price, setPrice] = useState('');
    const [country, setCountry] = useState('');
    const [result, setResult] = useState(null);
    const [message, setMessage] = useState('');
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setMessage('');
        setResult(null);

        try {
            const response = await fetch(`https://localhost:7005/api/Transact/scalar-function?price=${price}&country=${country}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                },
            });

            const data = await response.text();
            setMessage(data);
        } catch (error) {
            setMessage('An unexpected error occurred.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="function-container">
            <h2>Get Author Price Country Count</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="price">Price:</label>
                    <input
                        type="number"
                        id="price"
                        value={price}
                        onChange={(e) => setPrice(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="country">Country:</label>
                    <input
                        type="text"
                        id="country"
                        value={country}
                        onChange={(e) => setCountry(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" disabled={loading}>
                    {loading ? 'Fetching data...' : 'Get Count'}
                </button>
            </form>

            {message && <p>{message}</p>}

            {result !== null && (
                <div className="result-container">
                    <p><strong>Result:</strong> {result}</p>
                </div>
            )}
        </div>
    );
};

export default GetAuthorPriceCountryCount;
