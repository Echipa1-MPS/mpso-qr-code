package stepDefinitions;

import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;

public class StepDefinitions {

    @Given("^User validate the browser$")
    public void user_validate_the_browser(){
        System.out.println("User validate the browser!");
    }

    @When("^Browser is triggered$")
    public void browser_is_triggered(){
        System.out.println("Browser is triggered!");
    }

    @Then("^Check if browser is started$")
    public void check_if_browser_is_started(){
        System.out.println("Check if browser is started!");
    }
}
