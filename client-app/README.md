## Setup

Prerequisites:

- npm version 6 (version 7 causes dependencies mismatch)
- node version >= 10

To run application locally:

1. `npm install` - install project dependencies
2. `npm start` - runs app in dev mode

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.\
You will also see any lint errors in the console.

### `npm run test`

Launches the test runner in the interactive watch mode.\
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `npm run build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `npm run eject`

**Note: this is a one-way operation. Once you `eject`, you can’t go back!**

If you aren’t satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you’re on your own.

## Folder structure

**src/components** - UI (dumb) components
**src/modules** - complete modules with signle responsibility principle (e.g. auth, toastr,..)
**src/routes** - react-router routes & pages
**src/styles** - global styles
**src/store** - redux store configuration
**src/utils** - utility functions used across multiple modules

## Styling guide

In project we use sass pre-processor globally as well as modulary. For sass modules use convention {component name}.modules.scss and it should be located right next to actual component.

## Dependencies info

**React Router** - Routing library [documentation](https://reactrouter.com/web/guides/quick-start)
**Redux** - State management library [documentation](https://redux.js.org/introduction/getting-started), [best practice](https://redux.js.org/style-guide/style-guide)
**Redux thunk** - Used for redux side effect logic [documentation](https://github.com/reduxjs/redux-thunk)
**Reselect** - Memoization for redux selectors [documentation](https://github.com/reduxjs/reselect)
**@reduxjs/toolkit** - Redux toolkit for reducing boilerplate [documentation](https://redux-toolkit.js.org/introduction/getting-started)
**axios** - Http client for browser [documentation](https://github.com/axios/axios)
