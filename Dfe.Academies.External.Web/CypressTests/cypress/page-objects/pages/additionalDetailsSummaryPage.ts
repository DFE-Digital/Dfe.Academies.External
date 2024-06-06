class AdditionalDetailsSummaryPage {
  public selectAdditionalDetailsStartSection(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()

    return this
  }

  public additionalDetailsSummaryCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')
    cy.get('.govuk-heading-l').contains('Further information')

    cy.get('.govuk-heading-m').contains('Additional details')

    cy.get('.govuk-link').should('be.visible').contains('Change your answers')

    cy.get('b').eq(0).contains('What will the school bring to the trust they are joining?')
    // cy.get('p').eq(2).contains('What will the school bring to the trust they are joining? Describe the contribution they will make Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore ')

    cy.get('b').eq(1).contains('Have Ofsted recently inspected the school but not published the report yet?')
    // cy.get('p').eq(4).contains('No')

    cy.get('b').eq(2).contains('Are there any safeguarding investigations ongoing at the school?')
    cy.get('p').eq(6).contains('No')

    cy.get('b').eq(3).contains('Is the school part of a local authority reorganisation?')
    cy.get('p').eq(8).contains('No')

    cy.get('b').eq(4).contains('Is the school part of any local authority closure plans?')
    cy.get('p').eq(10).contains('No')

    cy.get('b').eq(5).contains('Is your school linked to a diocese?')
    cy.get('p').eq(12).contains('Yes')

    cy.get('b').eq(6).contains('Is the school part of a federation?')
    cy.get('p').eq(14).contains('No')

    cy.get('b').eq(7).contains('Is the school supported by a foundation, trust or other body (e.g. parish council) that appoints foundation governors?')
    cy.get('p').eq(16).contains('Yes')

    cy.get('b').eq(8).contains('Does the school currently have an exemption from providing broadly Christian collective worship issued by the local Standing Committee on Religious Education (SACRE)?')
    cy.get('p').eq(18).contains('No')

    cy.get('b').eq(9).contains('Provide a list of your main feeder schools')
    cy.get('p').eq(20).contains('Please provide a list of your main feeder schools We recognise you may have many feeder schools, therefore please just detail the top 5 Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do ')

    cy.get('b').eq(10).contains('The school\'s Governing Body must have passed a resolution to apply to convert to academy status. Upload a copy of the school\'s consent to converting and joining the trust.')
    cy.get('p').eq(22).contains('fiftyk.pdf')

    cy.get('b').eq(11).contains('Has an equalities impact assessment been carried out and considered by the governing body?')
    cy.get('p').eq(24).contains('No')

    cy.get('b').eq(12).contains('Do you want to add any further information?')
    cy.get('p').eq(26).contains('No')

    cy.get('.govuk-button').should('be.visible').contains('Back')

    return this
  }

  public submitAdditionalDetailsSummary(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const additionalDetailsSummaryPage = new AdditionalDetailsSummaryPage()

export default additionalDetailsSummaryPage
