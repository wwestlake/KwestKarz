import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const statusOptions = ['Not Listed', 'Listed', 'Reserved', 'Booked', 'On Trip', 'In Maintenance', 'Sold'];

function FleetPage({ token }) {
    const navigate = useNavigate();
    const [vehicles, setVehicles] = useState([]);
    const [form, setForm] = useState(getEmptyForm());
    const [filters, setFilters] = useState({ make: '', model: '', yearMin: '', yearMax: '', statuses: [] });

    function getEmptyForm() {
        return {
            id: null,
            make: '',
            model: '',
            year: '',
            color: '',
            plateNumber: '',
            status: 'Not Listed',
        };
    }

    function authFetch(url, options = {}) {
        return fetch(url, {
            ...options,
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`,
                ...(options.headers || {}),
            },
        });
    }

    useEffect(() => {
        loadVehicles();
    }, []);

    async function loadVehicles() {
        const res = await authFetch('/api/Vehicle');
        const data = await res.json();
        setVehicles(data);
    }

    function handleFormChange(e) {
        setForm({ ...form, [e.target.name]: e.target.value });
    }

    function handleFilterChange(e) {
        const { name, value, type, checked } = e.target;
        if (name === 'statuses') {
            const updated = checked
                ? [...filters.statuses, value]
                : filters.statuses.filter((s) => s !== value);
            setFilters({ ...filters, statuses: updated });
        } else {
            setFilters({ ...filters, [name]: value });
        }
    }

    function filteredVehicles() {
        return vehicles.filter(v => {
            const matchMake = v.make.toLowerCase().includes(filters.make.toLowerCase());
            const matchModel = v.model.toLowerCase().includes(filters.model.toLowerCase());
            const matchYearMin = !filters.yearMin || v.year >= parseInt(filters.yearMin);
            const matchYearMax = !filters.yearMax || v.year <= parseInt(filters.yearMax);
            const matchStatus = filters.statuses.length === 0 || filters.statuses.includes(v.status);
            return matchMake && matchModel && matchYearMin && matchYearMax && matchStatus;
        });
    }

    async function handleSubmit() {
        const method = form.id ? 'PUT' : 'POST';
        const url = form.id ? `/api/Vehicle/${form.id}` : '/api/Vehicle';
        const res = await authFetch(url, {
            method,
            body: JSON.stringify(form),
        });
        if (res.ok) {
            await loadVehicles();
            setForm(getEmptyForm());
        }
    }

    function handleSelect(vehicle) {
        setForm(vehicle);
        window.scrollTo({ top: 0, behavior: 'smooth' });
    }

    async function handleDelete(id) {
        if (!window.confirm('Delete this vehicle?')) return;
        const res = await authFetch(`/api/Vehicle/${id}`, { method: 'DELETE' });
        if (res.ok) {
            await loadVehicles();
            setForm(getEmptyForm());
        }
    }

    return (
        <div className="container py-4">
            <button className="btn btn-outline-secondary mb-3" onClick={() => navigate(-1)}>Back</button>

            <div className="card p-4 mb-4 shadow-sm">
                <h4>{form.id ? 'Edit Vehicle' : 'Add Vehicle'}</h4>
                <div className="row g-3 mb-3">
                    <div className="col-md-2">
                        <input className="form-control" name="make" placeholder="Make" value={form.make} onChange={handleFormChange} />
                    </div>
                    <div className="col-md-2">
                        <input className="form-control" name="model" placeholder="Model" value={form.model} onChange={handleFormChange} />
                    </div>
                    <div className="col-md-2">
                        <input className="form-control" name="year" placeholder="Year" type="number" value={form.year} onChange={handleFormChange} />
                    </div>
                    <div className="col-md-2">
                        <input className="form-control" name="color" placeholder="Color" value={form.color} onChange={handleFormChange} />
                    </div>
                    <div className="col-md-2">
                        <input className="form-control" name="plateNumber" placeholder="Plate" value={form.plateNumber} onChange={handleFormChange} />
                    </div>
                    <div className="col-md-2">
                        <select className="form-select" name="status" value={form.status} onChange={handleFormChange}>
                            {statusOptions.map(s => <option key={s} value={s}>{s}</option>)}
                        </select>
                    </div>
                </div>
                <div className="d-flex gap-2 justify-content-end">
                    <button className="btn btn-secondary" onClick={() => setForm(getEmptyForm())}>Clear</button>
                    <button className="btn btn-primary" onClick={handleSubmit}>Save</button>
                </div>
            </div>

            <div className="card p-4 mb-4 shadow-sm">
                <h5>Filter Vehicles</h5>
                <div className="row g-2 mb-3">
                    <div className="col-md-3">
                        <input className="form-control" name="make" placeholder="Make" value={filters.make} onChange={handleFilterChange} />
                    </div>
                    <div className="col-md-3">
                        <input className="form-control" name="model" placeholder="Model" value={filters.model} onChange={handleFilterChange} />
                    </div>
                    <div className="col-md-2">
                        <input className="form-control" name="yearMin" placeholder="Year Min" type="number" value={filters.yearMin} onChange={handleFilterChange} />
                    </div>
                    <div className="col-md-2">
                        <input className="form-control" name="yearMax" placeholder="Year Max" type="number" value={filters.yearMax} onChange={handleFilterChange} />
                    </div>
                    <div className="col-md-12">
                        {statusOptions.map(s => (
                            <label key={s} className="form-check form-check-inline">
                                <input className="form-check-input" type="checkbox" name="statuses" value={s}
                                    checked={filters.statuses.includes(s)} onChange={handleFilterChange} />
                                <span className="form-check-label">{s}</span>
                            </label>
                        ))}
                    </div>
                </div>
            </div>

            <table className="table table-bordered">
                <thead>
                    <tr>
                        <th>Year</th>
                        <th>Make</th>
                        <th>Model</th>
                        <th>Color</th>
                        <th>Plate</th>
                        <th>Status</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {filteredVehicles().map(v => (
                        <tr key={v.id} onClick={() => handleSelect(v)} style={{ cursor: 'pointer' }}>
                            <td>{v.year}</td>
                            <td>{v.make}</td>
                            <td>{v.model}</td>
                            <td>{v.color}</td>
                            <td>{v.plateNumber}</td>
                            <td>{v.status}</td>
                            <td>
                                <button className="btn btn-sm btn-danger" onClick={(e) => { e.stopPropagation(); handleDelete(v.id); }}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default FleetPage;
