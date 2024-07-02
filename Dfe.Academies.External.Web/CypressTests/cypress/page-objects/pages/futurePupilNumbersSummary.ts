class FuturePupilNumbersSummary {
  public startFuturePupilNumbers(): this {
    cy.get('a[class="govuk-button govuk-button--secondary"]').click()

    return this
  }

  // TODO all of these elements require proper Cypress tags
  public checkFuturePupilNumbersSummaryCompleted(): this {
    cy.get('p').eq(2).contains('999')

    cy.get('p').eq(4).contains('1499')

    cy.get('p').eq(6).contains('1999')

    cy.get('p').eq(8).contains('School Capacity Assumptions')

    cy.get('p').eq(10).contains('999')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const futurePupilNumbersSummary = new FuturePupilNumbersSummary()

export default futurePupilNumbersSummary
