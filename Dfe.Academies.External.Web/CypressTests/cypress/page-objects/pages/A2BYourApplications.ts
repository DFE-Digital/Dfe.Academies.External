import BasePage from "../BasePage"
export default class A2BYourApplications extends BasePage {
    static yourApplicationsElementsVisible()
    {
        cy.yourApplicationsElementsVisible()
    }

    static selectJAMNotStartedApplicationButSchoolAdded()
    {
        cy.selectJAMNotStartedApplicationButSchoolAdded()
    }

    static selectStartANewApplication()
    {
        cy.selectStartANewApplication()
    }

    static selectStartANewApplication()
    {
        cy.get('a[href="/what-are-you-applying-to-do"]').click()
    }
}