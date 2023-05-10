import BasePage from "../BasePage"
export default class A2BConsultationDetails extends BasePage {

static consultationDetailsElementsVisible()
{
    cy.consultationDetailsElementsVisible()
}

static fillConsultationDetails()
{
    cy.fillConsultationDetails()
}

static submitConsultationDetails()
{
    cy.submitConsultationDetails()
}

}