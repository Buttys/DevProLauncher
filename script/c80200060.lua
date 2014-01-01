--アーティファクト・ムーブメント
function c80200060.initial_effect(c)
	--Activate
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(80200060,0))
	e1:SetCategory(CATEGORY_DESTROY)
	e1:SetType(EFFECT_TYPE_ACTIVATE)
	e1:SetCode(EVENT_FREE_CHAIN)
	e1:SetHintTiming(0,TIMING_END_PHASE+TIMING_EQUIP)
	e1:SetProperty(EFFECT_FLAG_CARD_TARGET)
	e1:SetTarget(c80200060.target)
	e1:SetOperation(c80200060.activate)
	c:RegisterEffect(e1)
	--skip bp
	local e2=Effect.CreateEffect(c)
	e2:SetDescription(aux.Stringid(80200060,1))
	e2:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_F)
	e2:SetCode(EVENT_TO_GRAVE)
	e2:SetCondition(c80200060.bpcon)
	e2:SetOperation(c80200060.bpop)
	c:RegisterEffect(e2)
end
function c80200060.filter(c)
	return c:IsDestructable() and c:IsType(TYPE_SPELL+TYPE_TRAP)
end
function c80200060.setfilter(c)
	return c:IsSetCard(0x95) and c:IsType(TYPE_MONSTER) and c:IsSSetable()
end
function c80200060.target(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chkc then return chkc:IsOnField() and c80200060.filter(chkc) end
	if chk==0 then return Duel.IsExistingTarget(c80200060.filter,tp,LOCATION_ONFIELD,LOCATION_ONFIELD,1,e:GetHandler()) 
	and Duel.IsExistingMatchingCard(c80200060.setfilter,tp,LOCATION_DECK,0,1,nil) end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_DESTROY)
	local g=Duel.SelectTarget(tp,c80200060.filter,tp,LOCATION_ONFIELD,LOCATION_ONFIELD,1,1,e:GetHandler())
	Duel.SetOperationInfo(0,CATEGORY_DESTROY,g,1,0,0)
end
function c80200060.activate(e,tp,eg,ep,ev,re,r,rp)
	local tc=Duel.GetFirstTarget()
	if tc:IsRelateToEffect(e) and Duel.Destroy(tc,REASON_EFFECT) then
		Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_SET)
		local g=Duel.SelectMatchingCard(tp,c80200060.setfilter,tp,LOCATION_DECK,0,1,1,nil)
		if g:GetCount()>0 then
			local tc=g:GetFirst()
			Duel.SSet(tp,tc)
			Duel.ConfirmCards(1-tp,g)
		end
	end
end
function c80200060.bpcon(e,tp,eg,ep,ev,re,r,rp)
	return e:GetHandler():IsReason(REASON_DESTROY) and e:GetHandler():GetReasonPlayer()~=tp
		and e:GetHandler():GetPreviousControler()==tp
end
function c80200060.bpop(e,tp,eg,ep,ev,re,r,rp)
	local e1=Effect.CreateEffect(e:GetHandler())
	e1:SetType(EFFECT_TYPE_FIELD)
	e1:SetCode(EFFECT_SKIP_BP)
	e1:SetProperty(EFFECT_FLAG_PLAYER_TARGET)
	e1:SetTargetRange(0,1)
	if Duel.GetTurnPlayer()~=tp and Duel.GetCurrentPhase()==PHASE_BATTLE then
		e1:SetLabel(Duel.GetTurnCount())
		e1:SetCondition(c80200060.skipcon)
		e1:SetReset(RESET_PHASE+PHASE_BATTLE+RESET_OPPO_TURN,2)
	else
		e1:SetReset(RESET_PHASE+PHASE_BATTLE+RESET_OPPO_TURN,1)
	end
	Duel.RegisterEffect(e1,tp)
end
function c80200060.skipcon(e)
	return Duel.GetTurnCount()~=e:GetLabel()
end