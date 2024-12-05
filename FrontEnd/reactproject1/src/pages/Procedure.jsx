import React, { useState } from 'react';
import './css/Procedure.css';

const UpdateFilmDescription = () => {
    const [companyName, setCompanyName] = useState('');
    const [message, setMessage] = useState('');
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setMessage('');

        try {
            const response = await fetch('https://localhost:7005/api/Transact/procedure', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ CompanyName: companyName }),
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
        <div className="update-description-container">
            <h2>Update Film Descriptions</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="companyName">Company Name:</label>
                    <input
                        type="text"
                        id="companyName"
                        value={companyName}
                        onChange={(e) => setCompanyName(e.target.value)}
                        required
                    />
                </div>
                <button type="submit" disabled={loading}>
                    {loading ? 'Updating...' : 'Update Descriptions'}
                </button>
            </form>
            {message && <p>{message}</p>}
        </div>
    );
};

export default UpdateFilmDescription;
