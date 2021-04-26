import React, { useEffect } from 'react';
import ReactTooltip from 'react-tooltip';

import Routes from 'routes';

import 'components/tooltip/tooltip.scss';

function App() {
  return (
    <>
      <ReactTooltip className="custom-tooltip" delayShow={800} place="bottom" />
      <Routes />
    </>
  );
}

export default App;
