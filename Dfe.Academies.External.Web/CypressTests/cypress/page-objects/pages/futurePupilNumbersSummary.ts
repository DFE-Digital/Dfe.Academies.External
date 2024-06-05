class FuturePupilNumbersSummary {
  public futurePupilNumbersSummaryElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Future pupil numbers')

    cy.get('a[class="govuk-button govuk-button--secondary"]').should('be.visible').contains('Start section')

    cy.get('b').eq(0).contains('Projected pupil numbers on roll in the year the academy opens (year 1)')
    cy.get('p').eq(2).contains('You have not added any information')

    cy.get('b').eq(1).contains('Projected pupil numbers on roll in the following year after the academy has opened (year 2)')
    cy.get('p').eq(4).contains('You have not added any information')

    cy.get('b').eq(2).contains('Projected pupil numbers on roll in the following year (year 3)')
    cy.get('p').eq(6).contains('You have not added any information')

    cy.get('b').eq(3).contains('What do you base these projected numbers on?')
    cy.get('p').eq(8).contains('You have not added any information')

    cy.get('b').eq(4).contains('What is the school\'s published admissions number (PAN)?')
    cy.get('p').eq(10).contains('You have not added any information')

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application')

    return this
  }

  public selectFuturePupilNumbersStartSection(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()

    return this
  }

  public futurePupilNumbersSummaryCompleteElementsVisible(): this {
    cy.get('.govuk-back-link').contains('Back')

    cy.get('.govuk-caption-l').contains('Plymstock School')

    cy.get('.govuk-heading-l').contains('Future pupil numbers')

    cy.get('.govuk-link').should('be.visible').contains('Change your answers')

    cy.get('b').eq(0).contains('Projected pupil numbers on roll in the year the academy opens (year 1)')
    cy.get('p').eq(2).contains('999')

    cy.get('b').eq(1).contains('Projected pupil numbers on roll in the following year after the academy has opened (year 2)')
    cy.get('p').eq(4).contains('1499')

    cy.get('b').eq(2).contains('Projected pupil numbers on roll in the following year (year 3)')
    cy.get('p').eq(6).contains('1999')

    cy.get('b').eq(3).contains('What do you base these projected numbers on?')
    cy.get('p').eq(8).contains('School Capacity Assumptions')

    cy.get('b').eq(4).contains('What is the school\'s published admissions number (PAN)?')
    cy.get('p').eq(10).contains('999')

    cy.get('.govuk-button').should('be.visible').contains('Save and return to your application')

    return this
  }

  public submitFuturePupilNumbersSummary(): this {
    cy.get('.govuk-button').click()

    return this
  }
}

const futurePupilNumbersSummary = new FuturePupilNumbersSummary()

export default futurePupilNumbersSummary
