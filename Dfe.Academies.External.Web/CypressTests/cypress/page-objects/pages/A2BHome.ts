import BasePage from "../BasePage"
export default class A2BHome extends BasePage {
/*  NOW CALLING THESE FROM NEW HEADER COMPONENT 
    static govUkHeaderVisible() {
        cy.get('.govuk-header__logotype').should('be.visible').contains('GOV.UK')
    }

    static applyToBecomeAnAcademyHeaderLinkVisible() {
        cy.get('.govuk-header__content').contains('Apply to become an Academy')
    }

*/

static homePageElementsVisible()
{
    cy.get('h1').contains('Apply to become an Academy')
    cy.get('p').eq(2).contains('This form is for local authority maintained schools that want to become academies.')
    cy.get('p').eq(3).contains('There is a separate process for:')
    cy.get('li').eq(0).contains('special schools')
    cy.get('li').eq(1).contains('pupil referral units (PRUs)')
    cy.get('p').eq(4).contains('Although you can invite other people to help with this application, only the chair of governors will be able to submit it.')
    cy.get('.govuk-heading-l').contains('Before you start')
    cy.get('p').eq(5).contains('You must:')
    cy.get('a[target="_blank"]').eq(0).contains('carry out a consultation with your stakeholders (opens in new tab)')
    cy.get('li').eq(3).contains('complete an Equality Impact Assessment (EIA)')
    cy.get('a[target="_blank"]').eq(1).contains('provide bank details (opens in a new tab)')
    cy.get('p').eq(6).contains('You should also:')
    cy.get('a[target="_blank"]').eq(2).contains('your regional director (opens in a new tab)')
    cy.get('a[target="_blank"]').eq(3).contains('all of the information and evidence you will need (opens in a new tab)')
    cy.get('.govuk-grid-column-two-thirds > .govuk-button').should('be.visible')
    
}

    static clickStartNow() {
        cy.get('.govuk-grid-column-two-thirds > .govuk-button').click()
    }
}