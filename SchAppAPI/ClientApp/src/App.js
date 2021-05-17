import logo from './logo.svg';
import './App.css';
import Navigation from './components/Navigation/Navigation';
import Login from './components/Login/Login';
import Teacher from './components/TeacherManagement/Teacher';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

import CreateTeacher from './components/TeacherManagement/CreateTeacher';
import CreateLesson from './components/Lessons/CreateLesson';


function App() {
  return (
    <Router>
      <Switch>
        <Route path="/" exact component={Login}/>
        <Route path="/teacher" component={Teacher} />
        <Route path="/create_teacher" component={CreateTeacher} />
        <Route path="/lesson" component={ CreateLesson }/>
      </Switch>
    </Router>
  );
}

export default App;
