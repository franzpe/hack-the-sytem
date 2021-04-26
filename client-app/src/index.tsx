import React from 'react';
import ReactDOM from 'react-dom';
import reportWebVitals from './utils/reportWebVitals';
import App from 'modules/app/App';
import { Provider } from 'react-redux';
import store from 'store';
import history from 'utils/history';
import { Router } from 'react-router';

import 'styles/main.scss';
import configureAxios from 'utils/axios/configureAxios';

configureAxios(store.dispatch);

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
      <Router history={history}>
        <App />
      </Router>
    </Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log)WebStorageStateStore)
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
if (process.env.REACT_APP_LOG_MEASUREMENT === 'true') {
  reportWebVitals(console.log);
}
