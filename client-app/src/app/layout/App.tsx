import { Fragment, useEffect, useState } from "react";
import { Container } from "semantic-ui-react";
import { Activity } from "../models/activity";
import NavBar from "./NavBar";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import { v4 as uuid } from "uuid";
import agent from "../api/agent";
import LoadingComponent from "./LoadingComponent";

function App() {
  const [activites, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<
    Activity | undefined
  >(undefined);
  const [editMode, setEditMode] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    agent.Activities.list().then((response) => {
      let activities: Activity[] = [];
      response.forEach(activity => {
        activity.date = activity.date.split('T')[0];
        activities.push(activity);
      })
      setActivities(activities);
      setLoading(false);
    });
  }, []);

  function handleSelectActivity(id: string) {
    setSelectedActivity(activites.find((x) => x.id === id));
  }

  function handleCancleSelectActivity() {
    setSelectedActivity(undefined);
  }

  function handleFormOpen(id?: string) {
    id ? handleSelectActivity(id) : handleCancleSelectActivity();
    setEditMode(true);
  }

  function handleFormClose() {
    setEditMode(false);
  }

  function handleCreateOrEditActivity(activity: Activity) {
    activity.id
      ? setActivities([
          ...activites.filter((x) => x.id !== activity.id),
          activity,
        ])
      : setActivities([...activites, { ...activity, id: uuid() }]);
    setEditMode(false);
    setSelectedActivity(activity);
  }

  function handleDeleteActivity(id: string) {
    setActivities([...activites.filter((x) => x.id !== id)]);
  }

  if (loading) return <LoadingComponent content="lLoading app"/>

  return (
    <Fragment>
      <NavBar openForm={handleFormOpen} />
      <Container style={{ marginTop: "7em" }}>
        <ActivityDashboard
          activities={activites}
          selectedActivity={selectedActivity}
          selectActivity={handleSelectActivity}
          cancelSelectActivity={handleCancleSelectActivity}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm={handleFormClose}
          createOrEditActivity={handleCreateOrEditActivity}
          deleteActivity={handleDeleteActivity}
        />
      </Container>
    </Fragment>
  );
}

export default App;
