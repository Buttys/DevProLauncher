--武神－ヒルメ
function c80200024.initial_effect(c)
	c:SetUniqueOnField(1,0,80200024)
	c:EnableReviveLimit()
	--special summon
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(80200024,0))
	e1:SetType(EFFECT_TYPE_FIELD)
	e1:SetCode(EFFECT_SPSUMMON_PROC)
	e1:SetProperty(EFFECT_FLAG_UNCOPYABLE)
	e1:SetRange(LOCATION_HAND)
	e1:SetValue(1)
	e1:SetCondition(c80200024.spcon)
	e1:SetOperation(c80200024.spop)
	c:RegisterEffect(e1)
	--discard
	local e2=Effect.CreateEffect(c)
	e2:SetDescription(aux.Stringid(80200024,1))
	e2:SetCategory(CATEGORY_HANDES)
	e2:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_O)
	e2:SetProperty(EFFECT_FLAG_DELAY+EFFECT_FLAG_DAMAGE_STEP)
	e2:SetCode(EVENT_TO_GRAVE)
	e2:SetCondition(c80200024.con)
	e2:SetTarget(c80200024.tg)
	e2:SetOperation(c80200024.op)
	c:RegisterEffect(e2)
end
function c80200024.spfilter(c)
	return c:IsSetCard(0x88) and not c:IsCode(80200024) and c:IsAbleToRemoveAsCost()
end
function c80200024.spcon(e,c)
	if c==nil then return true end
	local tp=c:GetControler()
	return Duel.GetLocationCount(tp,LOCATION_MZONE)>0
		and Duel.IsExistingMatchingCard(c80200024.spfilter,tp,LOCATION_GRAVE,0,1,nil)
end
function c80200024.spop(e,tp,eg,ep,ev,re,r,rp,c)
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_REMOVE)
	local g=Duel.SelectMatchingCard(tp,c80200024.spfilter,tp,LOCATION_GRAVE,0,1,1,nil)
	Duel.Remove(g,POS_FACEUP,REASON_COST)
	
end
function c80200024.con(e,tp,eg,ep,ev,re,r,rp)
	local c=e:GetHandler()
	return rp~=tp and c:IsReason(REASON_DESTROY)
		and c:IsPreviousLocation(LOCATION_ONFIELD) and c:GetPreviousControler()==tp
		and e:GetHandler():GetSummonType()==SUMMON_TYPE_SPECIAL+1
end
function c80200024.tg(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chk==0 then return 
		Duel.GetFieldGroupCount(tp,LOCATION_HAND,0)>0 
	and Duel.GetFieldGroupCount(tp,0,LOCATION_HAND)>0 end
	
end
function c80200024.op(e,tp,eg,ep,ev,re,r,rp)
	if Duel.DiscardHand(tp,aux.TRUE,1,1,REASON_EFFECT+REASON_DISCARD) then
	Duel.BreakEffect()
	Duel.DiscardHand(1-tp,aux.TRUE,1,1,REASON_EFFECT+REASON_DISCARD)
	end
end