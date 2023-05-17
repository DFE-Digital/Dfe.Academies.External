import BasePage from "../BasePage"
export default class A2BPreopeningSupportGrantDetails extends BasePage {

static preopeningSupportGrantDetailsElementsVisible()
{
    cy.preopeningSupportGrantDetailsElementsVisible()
}

static selectToTheSchoolPreopeningSupportGrantDetails()
{
    cy.get('#pay-toSchool').click()
}

static verifyToTheSchoolPreopeningSupportGrantDetailsSectionDisplays()
{
    cy.verifyToTheSchoolPreopeningSupportGrantDetailsSectionDisplays()
}

static submitPreopeningSupportGrantDetails()
{
    cy.get('input[type="submit"]').click()
}

}