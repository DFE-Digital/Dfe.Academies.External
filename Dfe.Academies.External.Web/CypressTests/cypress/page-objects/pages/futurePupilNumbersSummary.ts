class FuturePupilNumbersSummary {
  public startFuturePupilNumbers(): this {
    cy.get('[data-cy="startSectionButton"]').click()

    return this
  }

  public checkFuturePupilNumbersSummaryCompleted(): this {
    cy.get('[data-cy="response"]').eq(0).contains('999')

    cy.get('[data-cy="response"]').eq(1).contains('1499')

    cy.get('[data-cy="response"]').eq(2).contains('1999')

    cy.get('[data-cy="response"]').eq(3).contains('School Capacity Assumptions')

    cy.get('[data-cy="response"]').eq(4).contains('999')

    return this
  }

  public saveAndReturnToApp(): this {
    cy.get('[data-cy="saveAndReturnButton"]').click()

    return this
  }
}

const futurePupilNumbersSummary = new FuturePupilNumbersSummary()

export default futurePupilNumbersSummary
