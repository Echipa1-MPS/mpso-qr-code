import {useState, useEffect} from 'react';
import { postLogin } from '../../helpers/apicaller';

export default function Login(props) {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = (e) => {
        e.preventDefault();
        if (username && password) {
            postLogin({email: username, password: password}, 
                successfulLogin, 
                failedLogin);
        }
    }

    const successfulLogin = (response) => {
        if (response && response.data) {
            // localStorage.setItem('user', response.data.jwt_token);
            // window.location.href = '/';
            props.onLoginSuccesful(response.data.jwt_token);
        }
    }

    const failedLogin = (response) => {
        console.log("Failed login");
        console.log(response);
    }

    return (
        <div className="flex-container-row-center full-height-vw">
            <div style={{width: "400px", marginTop: "50px"}}>
                <form>
                    <div className="form-group">
                        <label htmlFor="loginEmail">Email address</label>
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
                        <label htmlFor="loginPassword">Password</label>
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