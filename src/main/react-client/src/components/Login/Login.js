import {useState, useEffect} from 'react';

export default function Login() {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    useEffect(() => {
        if (localStorage.getItem('user')) {
            window.location.href = '/';
        }
    });

    const handleSubmit = (e) => {
        e.preventDefault();
        localStorage.setItem('user', username);
    }

    return (
        <div className="flex-container-row-center full-height-vw">
            <div style={{width: "400px", marginTop: "50px"}}>
                <form>
                    <div className="form-group">
                        <label for="loginEmail">Email address</label>
                        <input type="email"
                                value = {username}
                                onChange = {(e) => setUsername(e.target.value)}
                                className="form-control" 
                                id="loginEmail" 
                                aria-describedby="emailHelp" 
                                placeholder="Enter email"/>
                        <small id="emailHelp" className="form-text text-muted">Please login using the LDAP account</small>
                    </div>
                    <div className="form-group">
                        <label for="loginPassword">Password</label>
                        <input type="password"
                                value = {password}
                                onChange = {(e) => setPassword(e.target.value)}
                                className="form-control" 
                                id="loginPassword" 
                                placeholder="Password"/>
                    </div>
                    <button className="btn btn-primary" onClick = {handleSubmit}>Submit</button>
                </form>
            </div>
        </div>
    );
}